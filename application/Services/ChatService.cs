﻿using AutoMapper;
using SocialNetwork.Application.Contracts;
using SocialNetwork.Application.DTO;
using SocialNetwork.Application.Exceptions;
using SocialNetwork.Entities.Models;
using SocialNetworks.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocialNetwork.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        private readonly IBlobService _blobService;

        public ChatService(IRepositoryManager repository, IMapper mapper, IBlobService blobService)
        {
            _repository = repository;
            _mapper = mapper;
            _blobService = blobService;

        }
        public async Task AddMessage(int chatId, MessageForm messagedto)
        {
            if (messagedto == null) throw new InvalidDataException("message dto is null");
            var chat = await _repository.Chat.GetChatAsync(chatId, true);
            if (chat == null) throw new InvalidDataException("no such chat");
            var user = await _repository.User.GetUserAsync(messagedto.From, true);
            if (user == null) throw new InvalidDataException("no such user");
            var message = _mapper.Map<MessageForm, Message>(messagedto);
            message.User = user;
            chat.Messages.Add(message);
            await _repository.SaveAsync();
        }

        public async Task AddUser(int chatId, int userId)
        {
            var user = await _repository.User.GetUserAsync(userId, true);
            if (user == null) throw new InvalidDataException("no such user");
            var chat = await _repository.Chat.GetChatAsync(chatId, true);
            if (chat == null) throw new InvalidDataException("no such chat");
            if (chat.Users.Contains(user))
                chat.Users.Remove(user);
            else
                chat.Users.Add(user);
            await _repository.SaveAsync();
        }

        public async Task<Chat> CreateChat(ChatForm chatdto)
        {
            if (chatdto == null || chatdto.Users == null)
            {
                return null;
            }
            var chat = new Chat();
            chat.Users = new List<User>();
            foreach(var x in chatdto.Users)
            {
                var user = await _repository.User.GetUserAsync(x, true);
                if(user != null)chat.Users.Add(user);
            }
            _repository.Chat.Create(chat);
            await _repository.SaveAsync();
            return chat;
        }

        public async Task<Message> CreateMessage(MessageForm messagedto)
        {
            if (messagedto == null)
            {
                return null;
            }
            var message = _mapper.Map<Message>(messagedto);
            
            _repository.Message.Create(message);
            await _repository.SaveAsync();
            return message;
        }

        public async Task DeleteChat(int chatId)
        {
            var chat = await _repository.Chat.GetChatAsync(chatId, true);
            if (chat != null && chat.Messages != null)
                foreach(var x in chat.Messages)
                {
                    _repository.Message.Delete(x);
                }
            _repository.Chat.Delete(chat);
            await _repository.SaveAsync();
        }

        public async Task DeleteMessage(int messageId)
        {
            var message = await _repository.Message.GetMessageAsync(messageId, true);
            _repository.Message.Delete(message);
            await _repository.SaveAsync();
        }

        public async Task<ChatForResponseDTO> GetChat(int chatId)
        {
            var chat = await _repository.Chat.GetChatAsync(chatId, false);
            if (chat == null)
            {
                return null;
            }
            var chatdto = _mapper.Map<ChatForResponseDTO>(chat);
            return chatdto;
        }

        public async Task<List<ChatForResponseDTO>> GetChats(int userId)
        {
            var user = await _repository.User.GetUserAsync(userId, true);
            if (user == null) return null;
            var chats = await _repository.Chat.GetChatsAsync(user, false);
            if (chats == null)
            {
                return null;
            }
            var chatdto = _mapper.Map<List<ChatForResponseDTO>>(chats);
            return chatdto;
        }

        public async Task<MessageForResponseDTO> GetMessage(int messageId)
        {
            var message = await _repository.Message.GetMessageAsync(messageId, false);
            if (message == null)
            {
                return null;
            }
            var messagedto = _mapper.Map<MessageForResponseDTO>(message);
            messagedto.Content = await _blobService.GetBLobsAsync(message.Blobs.Select(x => x.Id), false);
            return messagedto;
        }

        public async Task<List<MessageForResponseDTO>> GetMessages(int chatId)
        {
            var messages = await _repository.Message.GetMessgesByChatIdAsync(chatId, false);
            if (messages == null)
            {
                return null;
            }
            var messagesdto = _mapper.Map<List<MessageForResponseDTO>>(messages);
            for(int i = 0; i < messages.Count(); i++)
            {
                messagesdto[i].Content = await _blobService.GetBLobsAsync(messages[i].Blobs.Select(x => x.Id), false);
            }
            return messagesdto;
        }
    }
}
