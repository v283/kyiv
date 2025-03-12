using System;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System.ComponentModel;

namespace kyiv.Models
{
    [Table("mark_table")]
    public partial class MarkModel : BaseModel, INotifyPropertyChanged
    {
        [PrimaryKey("id")]
        public Guid Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        private string name;

        [Column("user_name")]
        public string Name
        {
            get => name;
            set => SetField(ref name, value);
        }

        private string? topic;

        [Column("topic")]
        public string? Topic
        {
            get => topic;
            set => SetField(ref topic, value);
        }

        private DateTime? writenAt;

        [Column("writen_at")]
        public DateTime? WritenAt
        {
            get => writenAt;
            set => SetField(ref writenAt, value);
        }

        private string text;

        [Column("text")]
        public string Text
        {
            get => text;
            set => SetField(ref text, value);
        }

        private string images;

        [Column("images")]
        public string Images
        {
            get => images;
            set => SetField(ref images, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void SetField<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }
    }
}
