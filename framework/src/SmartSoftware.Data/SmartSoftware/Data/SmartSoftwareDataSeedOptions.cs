namespace SmartSoftware.Data;

public class SmartSoftwareDataSeedOptions
{
    public DataSeedContributorList Contributors { get; }

    public SmartSoftwareDataSeedOptions()
    {
        Contributors = new DataSeedContributorList();
    }
}
