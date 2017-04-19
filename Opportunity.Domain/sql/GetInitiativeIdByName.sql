--declare @name nvarchar(80) = 'Hackathons 2017';
select id from Initiative where Name = @name
