﻿namespace MinimalApi2.Aws.Models.Responses
{
    public class UserResponseModel
    {
        public string[]? Errors { get; set; }
        public bool IsSuccess { get; set; }

        public string? Token { get; set; }
    }
}
