

using Core.DomainModels;
using Core.Dtos;
using Core.Enums;
using Core.Interfaces.IServices.SystemIServices;
using Core.Interfaces.IServices.UsersIServices;
using Core.Interfaces.Specification;
using Core.Interfaces.Specifications;
using Infrastructure.Data.Specification;
using System;


namespace Infrastructure.Services.UsersServices
{

    public class UserService(IUnitOfWork uow, IEmailService _emailService) : IUserService
    {

        public async Task SendVerificationnCode(Guid userId, string userEmail, string Url)
        {
            Random generator = new Random();
            string code = generator.Next(0, 1000000).ToString("D6");

            // insert the code to the database
            var validationCode = new ValidationCode()
            {
                UserId = userId,
                Code = code,
                GeneratedDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddMinutes(5)
            };
            await uow.Repository<ValidationCode>().AddAsync(validationCode);
            await uow.Complete();

            var subject = "كود التحقق";

            await _emailService.SendEmailAsync(new List<string> { userEmail }, subject, code);

        }


        public async Task<VerificationCodeValidationResult> ValidateVerificationCode(Guid userId, string verificationCode)
        {

            var validationCode = (await uow.Repository<ValidationCode>().GetAllWithSpecAsync(new GetValidationCodeSpec(userId))).FirstOrDefault();

            if (validationCode == null)
            {
                return VerificationCodeValidationResult.Invalid;
            }
            else if (validationCode.ExpirationDate < DateTime.Now)
            {
                return VerificationCodeValidationResult.Expired;
            }
            else if (validationCode.Code != verificationCode)
            {
                return VerificationCodeValidationResult.Invalid;
            }

            return VerificationCodeValidationResult.Valid;
        }
    }
}
