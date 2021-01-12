using GameException;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using GuildChat.Models.v1;
using GuildChat.Services.v1;

namespace GuildChat.Business.v1
{
    public class MessageBusiness : IMessageBusiness
    {
        private IMongoDB<Message> _mongoDB;
        public MessageBusiness(IMongoDB<Message> mongoDB)
        {
            _mongoDB = mongoDB;
        }

        public async Task<Message> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ParameterException("Message ID is null");
            }

            try
            {
                var filter = new FilterDefinitionBuilder<Message>().Eq(m => m.Id, id);
                Message message = await _mongoDB.GetById(filter);
                if (message == null)
                {
                    throw new NotFoundException("Message nor found");
                }
                return message;
            }
            catch (NotFoundException ne)
            {
                throw ne;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IEnumerable<Message>> GetLastTweenty(string guildId)
        {
            if (string.IsNullOrEmpty(guildId))
            {
                throw new ParameterException("Guild ID is null");
            }

            try
            {
                var filter = new FilterDefinitionBuilder<Message>().Eq(c => c.GuildId, guildId);
                IEnumerable<Message> messages = await _mongoDB.GetLastTweenty(filter);
                if (messages == null)
                {
                    messages = new List<Message>();
                }
                return messages;
            }
            catch (NotFoundException ne)
            {
                throw ne;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Message> Save(Message message)
        {
            message.Id = Guid.NewGuid().ToString();
            message.SendAt = DateTime.UtcNow;
            ValidateMessage(message);
            try
            {
                await _mongoDB.Save(message);
                return message;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
        
        public async Task Delete(string id, string playerId)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ParameterException("Id is null");
            }
            if (string.IsNullOrEmpty(playerId))
            {
                throw new ParameterException("Player Id is null");
            }

            var messageOld = await GetById(id);
            if (!messageOld.PlayerId.Equals(playerId))
            {
                throw new Exception("Only the message owner can excluded");
            }

            try
            {
                var filter = new FilterDefinitionBuilder<Message>().Eq(g => g.Id, id);
                Message messageDeleted = await _mongoDB.Delete(filter);
                if (messageDeleted == null)
                {
                    throw new NotFoundException("Message not found");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void ValidateMessage(Message message)
        {
            if (message == null)
            {
                throw new ParameterException("Message is null");
            }
            if (string.IsNullOrEmpty(message.Id))
            {
                throw new ParameterException("Message ID is null or empty");
            }
            if (string.IsNullOrEmpty(message.PlayerId))
            {
                throw new ParameterException("Message PlayerId is null or empty");
            }
            if (string.IsNullOrEmpty(message.GuildId))
            {
                throw new ParameterException("Message ChatId is null or empty");
            }
            if (string.IsNullOrEmpty(message.Text))
            {
                throw new ParameterException("Message Text is null or empty");
            }
            if (message.SendAt == null)
            {
                throw new ParameterException("Message SendAt is null");
            }
        }
    }
}
