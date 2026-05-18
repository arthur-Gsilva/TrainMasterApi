using TrainMaster.Domain.Common;


namespace TrainMaster.Domain.Entities;

public class Muscle : BaseEntity
{
    public string Name { get; private set; } = string.Empty;

    public ICollection<SubGroup> SubGroups { get; private set; } = new List<SubGroup>();


    private Muscle() {}

    public static Muscle Create(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new Muscle
        {
            Name = name
        };
    }

    public void Update(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
        SetUpdatedAt();
    }
}