/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET IDENTITY_INSERT Category ON
INSERT INTO Category (Id, Name, Icon)
VALUES (1, 'Secondment', 'secondment'),
       (2, 'Project', 'project'),
       (3, 'Communication', 'communication'),
       (4, 'Networking', 'networking'),
       (5, 'Analysis', 'analysis'),
       (6, 'Research', 'research');
SET IDENTITY_INSERT Category OFF
