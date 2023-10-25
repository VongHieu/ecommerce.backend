﻿using shopecommerce.Domain.Commons;
using shopecommerce.Domain.Commons.Commands;
using shopecommerce.Domain.Interfaces;
using shopecommerce.Domain.Models;
using shopecommerce.Domain.Resources;
using System.Net;

namespace shopecommerce.Application.Commands.UserCommand.LoginUser
{
    public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, BaseResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;
        public LoginUserCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<BaseResponseDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            //check user by phone number
            var user = await _userRepository.GetUserByPhoneNumber(request.phone_number);
            if(user == null)
            {
                return new BaseResponseDto(false, UserMessages.unauthorized, (int)HttpStatusCode.Unauthorized);
            }

            //check password
            if(!PasswordHasher.VerifyPassword(user.password, request.password))
            {
                user.SetLoginFaileCount();
                await _userRepository.UnitOfWork.SaveEntitiesChangeAsync(cancellationToken);

                return new BaseResponseDto(false, UserMessages.unauthorized, (int)HttpStatusCode.Unauthorized);
            }

            //save refreshtoken
            user.SetRefreshToken(_jwtProvider.GenerateRefreshToken());

            // create session
            var accessToken = await _jwtProvider.GenerateAccessTokenAsync(user);

            //save cookie token
            _jwtProvider.SaveCookiesStorage(accessToken);

            await _userRepository.UnitOfWork.SaveEntitiesChangeAsync(cancellationToken);
            return new BaseResponseDto(true, "Đăng nhập thành công", (int)HttpStatusCode.OK, accessToken);
        }
    }
}
