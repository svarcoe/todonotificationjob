using System;

public class TodoModel
{
    public string Id {get;set;}
    public string Title {get;set;}
    public string AssignedUserName {get;set;}
    public string AssignedUserEmail {get;set;}
    public DateTimeOffset DueDate {get;set;}
}