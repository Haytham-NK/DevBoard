namespace DevBoard.Api.DTOs
{
    public sealed class ProjectDto
    {
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;
        public string? Description { get; init; }
        public DateTime CreatedAt { get; init; }
    }
}
