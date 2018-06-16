// <copyright file="ArticleInformation.cs" company="Godsend Team">
// Copyright (c) Godsend Team. All rights reserved.
// </copyright>

namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Newtonsoft.Json;

    public class ArticleInformation : Information
    {
        [JsonIgnore]
        public IdentityUser EFAuthor { get; set; }

        [NotMapped]
        public ClientUser Author { get => EFAuthor == null ? null : new ClientUser { Id = EFAuthor.Id, Name = EFAuthor.UserName }; }

        public DateTime Created { get; set; }

        [JsonIgnore]
        public IEnumerable<StringWrapper> EFTags { get; set; }

        [NotMapped]
        public IEnumerable<string> Tags
        {
            get => this.EFTags?.Select(x => x.Value)?.ToArray();
            set => this.EFTags = value?.Select(s => new StringWrapper { Id = Guid.NewGuid(), Value = s }).ToArray();
        }
    }
}
