using System;
using MauiEfCorePOC.ValueComparer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MauiEfCorePOC.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public ICollection<Post> Posts { get; set; }
    }

    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Url).IsRequired();
        }
    }

    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public byte[] RowVersion { get; set; } // Example byte array column
    }

    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        private readonly ByteArrayValueComparer _byteArrayValueComparer;

        public PostConfiguration(ByteArrayValueComparer byteArrayValueComparer)
        {
            _byteArrayValueComparer = byteArrayValueComparer;
        }

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title).IsRequired();

            // Apply the custom ValueComparer to the byte array column
            builder.Property(p => p.RowVersion)
                   .IsRowVersion()
                   .HasConversion<byte[]>()
                   .Metadata.SetValueComparer(_byteArrayValueComparer);
        }
    }

}

