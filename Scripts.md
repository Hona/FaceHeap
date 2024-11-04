# Hello FaceHeap Developers 👋

## Welcome to the Developer Scripts

### Seed database

```sql
USE [faceheap]
GO

INSERT INTO [dbo].[Developers]
           ([Id], [Name])
     VALUES
           (NEWID(), 'Bryden'),
		   (NEWID(), 'Luke')
          
GO
```