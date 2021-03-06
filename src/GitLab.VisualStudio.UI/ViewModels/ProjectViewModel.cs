﻿using GitLab.VisualStudio.Shared;
using GitLab.VisualStudio.Shared.Controls;
using System;

namespace GitLab.VisualStudio.UI.ViewModels
{
    public class Owner : IEquatable<Owner>
    {
        public string Name { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsExpanded { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var p = (Owner)obj;
            return Name == p.Name;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public bool Equals(Owner other)
        {
            return Name == other.Name;
        }
    }

    public class ProjectViewModel
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public Owner Owner { get; set; }
        public Octicon Icon { get; set; }

        public bool IsActive { get; set; }

        public ProjectViewModel(Project repository)
        {
            Name = repository.Name;
            Url = repository.Url;

            if (repository.Owner != null)
            {
                Owner = new Owner
                {
                    Name = repository.Owner.Name,
                    AvatarUrl = repository.Owner.AvatarUrl
                };
            }

            Icon = repository.Icon;
        }
    }
}
