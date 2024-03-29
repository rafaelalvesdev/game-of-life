﻿using Microsoft.Extensions.Logging;

namespace GameOfLife.Application.Services
{
    public abstract class BaseService
    {
        protected readonly ILogger Logger;

        protected BaseService(ILogger logger)
        {
            Logger = logger;
        }
    }
}
