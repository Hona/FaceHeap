# Hello FaceHeap Developers 👋

## Welcome to the Developer Scripts

### Seed database

```sql
INSERT INTO [faceheap].[dbo].[Developers]
           ([Id], [Name], [Popularity])
     VALUES
           (NEWID(), 'Bryden', 50),
		   (NEWID(), 'Luke', 50)
GO
```