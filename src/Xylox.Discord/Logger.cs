﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;

namespace Xylox.Discord
{
    public class Logger
    {
        private readonly SemaphoreSlim _semaphoreSlim;

        public Logger()
        {
            _semaphoreSlim = new SemaphoreSlim(1);
        }

        internal async Task LogAsync(LogMessage arg)
        {
            await _semaphoreSlim.WaitAsync();

            var timeStamp = DateTime.UtcNow.ToString("MM/dd/yyyy hh:mm tt");
            const string format = "{0,-10} {1,10}";

            Console.WriteLine($"[{timeStamp}] {string.Format(format, arg.Source, $": {arg.Message}")}");

            _semaphoreSlim.Release();
        }
    }
}
