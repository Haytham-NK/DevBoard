namespace DevBoard.Api.DTOs
{
    public sealed class UpdateProjectDto
    {
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
    }
}