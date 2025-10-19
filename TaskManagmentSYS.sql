SELECT TOP (1000) [TaskID]
      ,[Title]
      ,[Description]
      ,[Status]
      ,[DueDate]
      ,[Userid]
  FROM [TasksDB].[dbo].[Tasks]

 truncate table [Tasks]

select * from [User]
select * from Tasks
