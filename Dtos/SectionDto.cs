namespace DashboardApi;

public class SectionDto
{
    public string? GridConfig { get; set; }

    public SectionDto()
    {
        if (GridConfig == null)
        {
            GridConfig = "";
        }
    }


}
