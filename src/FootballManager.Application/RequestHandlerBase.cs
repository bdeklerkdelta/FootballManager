﻿using Microsoft.Extensions.Logging;
using System;
using FootballManager.Application.Models;
using FootballManager.Application.ResponseNotifications.ErrorResponses;
using FootballManager.Common.Notifications;
using FootballManager.Domain.Exceptions;
using System.Threading.Tasks;
using FootballManager.Persistence;

namespace FootballManager.Application
{
    public abstract class RequestHandlerBase
    {
        protected Guid CallReference { get; set; } 

        protected ILogger Logger { get; private set; }

        protected RequestHandlerBase(ILogger logger)
        {
            Logger = logger;
        }

        public async Task<TResult> ExecuteAsync<TResult>(Func<TResult, Task> action) where TResult : NotificationViewModel, new()
        {
            CallReference = Guid.NewGuid();
            
            var result = new TResult();

            try
            {
                await action(result);
            }
            catch (Exception ex)
            {
                var notification = ErrorResponseNotifications.GeneralErrors.Unhandled(CallReference);
                result.Notifications += ErrorResponseNotifications.GeneralErrors.Unhandled(CallReference);
                Logger.LogError(ex, notification.ToString());
            }

            return result;
        }

        public TResult Execute<TResult>(Action<TResult> action) where TResult : NotificationViewModel, new()
        {
            CallReference = Guid.NewGuid();

            var result = new TResult();

            try
            {  
                action(result);
            }
            catch (DomainValidationException ex)             
            {
                var notification = Notification.Create(ex.Message, NotificationSeverity.Warning);
                result.Notifications += notification;
                Logger.LogWarning(ex, notification.ToString());
            } 
            catch (Exception ex)
            {
                var notification = ErrorResponseNotifications.GeneralErrors.Unhandled(CallReference);
                result.Notifications += notification;
                Logger.LogError(ex, notification.ToString());
            }            

            return result;
        } 
    }
}
