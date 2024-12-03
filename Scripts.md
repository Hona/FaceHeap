# Hello FaceHeap Developers 👋

## Welcome to the Developer Scripts

### Seed database

```sql
INSERT INTO [faceheap].[dbo].[Developers]
           ([Id], [Name])
     VALUES
           (NEWID(), 'Bryden'),
		   (NEWID(), 'Luke')
GO
```