using System;
using SQLite;

namespace Login.Models
{
    [Table("Item")]
    public class Item
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}