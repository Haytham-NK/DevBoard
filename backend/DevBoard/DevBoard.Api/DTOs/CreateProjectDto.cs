namespace DevBoard.Api.Dtos
{
    public sealed class CreateProjectDto
    {
        public string Name { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
    }
}
