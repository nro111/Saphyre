﻿using Application.Interfaces;
using Contracts;
using DataAccess.Context;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Shared;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SaphyreContext _context;
        private readonly ILogger<IUserRepository> _logger;
        private readonly Supabase.Client _supabaseClient;

        public UserRepository(
            SaphyreContext context,
            ILogger<IUserRepository> logger,
            Supabase.Client supabaseClient)
        {
            _context = context;
            _logger = logger;
            _supabaseClient = supabaseClient;
        }

        public async Task<OperationResult<bool>> Create(UserDTO user)
        {
            try
            {
                await _context
                    .Set<User>()
                    .AddAsync(
                        new User()
                        {
                            Id = user.Id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            Email = user.Email,
                            Phone = user.Phone,
                            AddressLine1 = user.AddressLine1,
                            AddressLine2 = user.AddressLine2,
                            City = user.City,
                            State = user.State,
                            PostalCode = user.PostalCode,
                            DateOfBirth = user.DateOfBirth,
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow,
                        });

                await _context.SaveChangesAsync();

                return OperationResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return OperationResult<bool>.FailureResult("Unexpected error", OperationStatus.InternalError);
            }
        }

        public async Task<OperationResult<bool>> Delete(Guid id)
        {
            try
            {
                var userToDelete = await _context
                    .Set<User>()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (userToDelete == null)
                    return OperationResult<bool>.FailureResult("User not found", OperationStatus.NotFound);

                _context.Set<User>().Remove(userToDelete);

                await _context.SaveChangesAsync();

                return OperationResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return OperationResult<bool>.FailureResult("Unexpected error", OperationStatus.InternalError);
            }
        }

        public async Task<OperationResult<UserDTO>> Get(Guid id)
        {
            try
            {
                var foundUser = await _context
                    .Set<User>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (foundUser == null)
                    return OperationResult<UserDTO>.FailureResult("User not found", OperationStatus.NotFound);

                return OperationResult<UserDTO>.SuccessResult(
                        new UserDTO()
                        {
                            Id = foundUser.Id,
                            FirstName = foundUser.FirstName,
                            LastName = foundUser.LastName,
                            Email = foundUser.Email,
                            Phone = foundUser.Phone,
                            AddressLine1 = foundUser.AddressLine1,
                            AddressLine2 = foundUser.AddressLine2,
                            City = foundUser.City,
                            State = foundUser.State,
                            PostalCode = foundUser.PostalCode,
                            DateOfBirth = foundUser.DateOfBirth
                        }
                    );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return OperationResult<UserDTO>.FailureResult("Unexpected error", OperationStatus.InternalError);
            }
        }

        public async Task<OperationResult<List<UserDTO>>> GetAll()
        {
            try
            {
                var allUsers = await _context
                    .Set<User>()
                    .AsNoTracking()
                    .Select(x =>
                        new UserDTO()
                        {
                            Id = x.Id,
                            FirstName = x.FirstName,
                            LastName = x.LastName,
                            Email = x.Email,
                            Phone = x.Phone,
                            AddressLine1 = x.AddressLine1,
                            AddressLine2 = x.AddressLine2,
                            City = x.City,
                            State = x.State,
                            PostalCode = x.PostalCode,
                            DateOfBirth = x.DateOfBirth
                        })
                    .ToListAsync();

                if (allUsers == null)
                    return OperationResult<List<UserDTO>>.FailureResult("No users found", OperationStatus.NotFound);

                return OperationResult<List<UserDTO>>.SuccessResult(allUsers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return OperationResult<List<UserDTO>>.FailureResult("Unexpected error", OperationStatus.InternalError);
            }
        }

        public async Task<OperationResult<bool>> Register(RegistrationDTO dto)
        {
            try
            {

                var response = await _supabaseClient.Auth.SignUp(dto.Email, dto.Password);
                if (response.User == null)
                    return OperationResult<bool>.FailureResult("Failed to register with Supabase", OperationStatus.InternalError);

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Phone = string.Empty,
                    AddressLine1 = string.Empty,
                    AddressLine2 = string.Empty,
                    City = string.Empty,
                    State = string.Empty,
                    PostalCode = string.Empty,
                    DateOfBirth = DateTime.MinValue,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _context.Set<User>().AddAsync(user);
                await _context.SaveChangesAsync();

                return OperationResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return OperationResult<bool>.FailureResult("Unexpected error", OperationStatus.InternalError);
            }
        }

        public async Task<OperationResult<bool>> Login(AuthenticationDTO dto)
        {
            try
            {
                var response = await _supabaseClient.Auth.SignIn(dto.Email, dto.Password);
                if (response.User == null)
                    return OperationResult<bool>.FailureResult("Invalid credentials", OperationStatus.Unauthorized);

                return OperationResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return OperationResult<bool>.FailureResult("Unexpected error", OperationStatus.InternalError);
            }
        }


        public async Task<OperationResult<bool>> Update(UserDTO user)
        {
            try
            {
                var foundUser = await _context
                    .Set<User>()
                    .FirstOrDefaultAsync(x => x.Id == user.Id);

                if (foundUser == null)
                    return OperationResult<bool>.FailureResult("User not found", OperationStatus.NotFound);

                foundUser.AddressLine1 = user.AddressLine1;
                foundUser.AddressLine2 = user.AddressLine2;
                foundUser.City = user.City;
                foundUser.State = user.State;
                foundUser.PostalCode = user.PostalCode;
                foundUser.DateOfBirth = user.DateOfBirth;
                foundUser.Email = user.Email;
                foundUser.FirstName = user.FirstName;
                foundUser.LastName = user.LastName;
                foundUser.Phone = user.Phone;
                foundUser.UpdatedAt = DateTime.UtcNow;

                _context
                    .Set<User>()
                    .Update(foundUser);

                await _context
                    .SaveChangesAsync();

                return OperationResult<bool>.SuccessResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return OperationResult<bool>.FailureResult("Unexpected error", OperationStatus.InternalError);
            }
        }
    }
}
