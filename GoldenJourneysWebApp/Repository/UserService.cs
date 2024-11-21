﻿using System.Web.Helpers;
using System.Web.Mvc;
using GoldenJourneysWebApp.Data;
using GoldenJourneysWebApp.Data.Entities;
using GoldenJourneysWebApp.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GoldenJourneysWebApp.Repository
{
    public class UserService : IUserService
    {
        public readonly ApplicationDBContext _context;
        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }
        public List<UserViewModel> GetUsers()
        {
            return _context.Users.Select(x => new UserViewModel
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                Type = x.UserType.Type,
                Status = x.Status,
                CreatedAt = x.CreatedAt,
            }).ToList();
        }
		public List<UserViewModel> FilterUsers(string userType)
		{
			return _context.Users.Where(u => u.UserType.Type == userType)
                .Select(x => new UserViewModel
			{
				Id = x.Id,
				UserName = x.UserName,
				Email = x.Email,
				PhoneNumber = x.PhoneNumber,
				Address = x.Address,
				Type = x.UserType.Type,
				Status = x.Status,
				CreatedAt = x.CreatedAt,
			}).ToList();
		}
		public bool IsEmailUnique(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }
        public void AdminCreate(AdminCreateViewModel admin)
        {
            User Admin = new User
            {
                UserName = admin.UserName,
                Email = admin.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(admin.Password),
                PhoneNumber = admin.PhoneNumber,
                Address = admin.Address,
                UserTypeId = 2,
                Status = "Pending",
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
            };
            _context.Users.Add(Admin);
            _context.SaveChanges();
        }
        public void CustomerRegister(CustomerRegistrationViewModel customer)
        {
            User Customer = new User
            {
                UserName = customer.UserName,
                Email = customer.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(customer.Password),
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                UserTypeId = 1,
                Status = "Active",
                CreatedAt = DateOnly.FromDateTime(DateTime.Now),
            };
            _context.Users.Add(Customer);
            _context.SaveChanges();
        }
        public UserViewModel GetUserByEmail(string userEmail)
        {
            var user = _context.Users.Where(u => u.Email == userEmail)
                .Select(x => new UserViewModel
                {
					Id = x.Id,
					UserName = x.UserName,
					Email = x.Email,
					PhoneNumber = x.PhoneNumber,
					Address = x.Address,
					Type = x.UserType.Type,
					CreatedAt = x.CreatedAt,
				}).SingleOrDefault();
            return user;
        }
        public void UserEdit(string userEmail, UserViewModel user)
        {
            User existingUser = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            {
                existingUser.UserName = user.UserName;
                existingUser.Email = user.Email;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.Address = user.Address;
                existingUser.Status = user.Status;
                _context.Update(existingUser);
                _context.SaveChanges();
            }
        }
        public bool ValidateUser(string Email, string Password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == Email && x.Status == "Active");

            if (user != null)
            {
                return BCrypt.Net.BCrypt.Verify(Password, user.Password);
            }
            return false;
        }
    }
}
