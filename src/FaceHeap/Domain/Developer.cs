namespace FaceHeap.Domain;

public class Developer
{
    public DeveloperId Id { get; init; } = DeveloperId.FromNewGuid();
    public string Name { get; set; }
    public int Popularity { get; set; }
}
