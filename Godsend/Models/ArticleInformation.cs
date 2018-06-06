﻿namespace Godsend.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;

    public class ArticleInformation : Information
    {
        public IdentityUser Author { get; set; }

        public DateTime Created { get; set; }

        public IEnumerable<StringWrapper> EFTags { get; set; }

        [NotMapped]
        public IEnumerable<string> Tags
        {
            get => EFTags.Select(x => x.Value);
            set => EFTags = value.Select(s => new StringWrapper { Id = Guid.NewGuid(), Value = s });
        }
    }
}
