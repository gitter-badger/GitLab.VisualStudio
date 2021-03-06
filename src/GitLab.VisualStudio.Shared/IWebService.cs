﻿using GitLab.VisualStudio.Shared.Controls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitLab.VisualStudio.Shared
{
    public enum ApiVersion
    {
        V3,
        V4,
        V3_Oauth,
        V4_Oauth
    }
    public class User
    {
        public static implicit operator User(NGitLab.Models.Session session)
        {
            return (NGitLab.Models.User)session;
        }
        public static implicit operator User(NGitLab.Models.User session)
        {
            if (session != null)
            {
                return new User()
                {
                    AvatarUrl = session.AvatarUrl,
                    Email = session.Email,
                    Id = session.Id,
                    Name = session.Name,
                    TwoFactorEnabled = session.TwoFactorEnabled,
                    Username = session.Username,
                };
            }
            else
            {
                return null;
            }
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }
        public string PrivateToken { get; set; }
        public string Host { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public ApiVersion ApiVersion { get; set; }

    }

    public class Project
    {
        public static implicit operator Project(NGitLab.Models.Project p)
        {
            if (p != null)
            {
                return new Project()
                {
                    BuildsEnabled = p.BuildsEnabled,
                    Fork = p.Fork,
                    HttpUrl = p.HttpUrl,
                    IssuesEnabled = p.IssuesEnabled,
                    Name = p.Name,
                    Owner = p.Owner,
                    Public = p.Public,
                    Path = p.Path,
                    MergeRequestsEnabled = p.MergeRequestsEnabled,
                    SnippetsEnabled = p.SnippetsEnabled,
                    SshUrl = p.SshUrl,
                    WikiEnabled = p.WikiEnabled,
                    Id = p.Id,
                    WebUrl = p.WebUrl
                };
                
            }
            else
            {
                return null;
            }
        }

        public int Id { get; set; }

    
        public string Name { get; set; }

        public string Path { get; set; }

        public bool Public { get; set; }
        public string SshUrl { get; set; }
        public string HttpUrl { get; set; }
        public string WebUrl { get; set; }
        public User Owner { get; set; }

        public bool Fork { get; set; }

        public bool IssuesEnabled { get; set; }

        public bool MergeRequestsEnabled { get; set; }

        public bool WikiEnabled { get; set; }
        public bool BuildsEnabled { get; set; }
        public bool SnippetsEnabled { get; set; }

        public string Url
        {
            get { return HttpUrl; }
        }

     
        public string LocalPath { get; set; }

 
        public Octicon Icon
        {
            get
            {
                return Public ? Octicon.@lock
                    : Fork
                    ? Octicon.repo_forked
                    : Octicon.repo;
            }
        }
    }
    public class  Snippet
    {
        public static implicit operator Snippet(NGitLab.Models.ProjectSnippet p)
        {
            if (p != null)
            {
                return new Snippet()
                {
                    Description = p.Description,
                    FileName = p.FileName,
                    Id = p.Id,
                    Title = p.Title,
                    WebUrl = p.WebUrl
                };
            }
            else
            {
                return null;
            }
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string WebUrl { get; set; }
    }
    public class CreateProjectResult 
    {
        public string Message { get; set; }
        public Project Project { get; set; }
    }
    public class NamespacesPath
    {
        public static implicit operator NamespacesPath(NGitLab.Models.Namespaces p)
        {
            if (p != null)
            {
                return new NamespacesPath()
                {
                    id = p.Id,
                    kind = p.Kind,
                    name = p.Name,
                    path = p.Path,
                    full_path = p.FullPath
                };
            }
            else
            {
                return null;
            }
        }
        
        public int id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public string kind { get; set; }
        public string full_path { get; set; }
   
    }
    public class CreateSnippetResult
    {
        public string Message { get; set; }
        public Snippet Snippet { get; set; }
    }
    public enum ProjectListType
    {
        Accessible,
        Owned,
        Membership,
        Starred,
        Forked
    }
    public interface IWebService
    {
        User LoginAsync(bool enable2fa, string host,string email, string password,ApiVersion apiVersion);
        IReadOnlyList<Project> GetProjects();
         CreateProjectResult CreateProject(string name, string description, bool isPrivate, int  namespaceid);
         CreateProjectResult CreateProject(string name, string description, bool isPrivate);
        CreateSnippetResult CreateSnippet(string title, string filename, string description, string code, string visibility);
        Project GetActiveProject();
        Project GetActiveProject(ProjectListType projectListType);
        IReadOnlyList<Project> GetProjects(ProjectListType projectListType);
        Project GetProject(string namespacedpath);
        IReadOnlyList<NamespacesPath> GetNamespacesPathList();
    }
}
