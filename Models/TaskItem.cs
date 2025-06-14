﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TaskManager_Backend.Models
{
    public class TaskItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
    }
}
