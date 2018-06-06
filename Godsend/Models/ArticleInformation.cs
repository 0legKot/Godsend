namespace Godsend.Models
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

        public IEnumerable<Wrapper<string>> EFTags { get; set; }

        [NotMapped]
        public IEnumerable<string> Tags
        {
            get => this.EFTags?.Select(x => x.Value)?.ToArray();
            set => this.EFTags = value?.Select(s => new Wrapper<string> { Id = Guid.NewGuid(), Value = s }).ToArray();
        }
    }
}
