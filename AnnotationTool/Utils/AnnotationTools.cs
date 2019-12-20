using Prism.Events;

namespace AnnotationTool.Utils
{
    public class AnnotationToolSelectedEvent : PubSubEvent<string> { }

    public interface IAnnotationTool
    {
        string ToolName { get; }
    }

    public class BoundingBoxTool : IAnnotationTool
    {
        public string ToolName { get; } = "Bounding Box";
    }

    public class LandmarksTool : IAnnotationTool
    {
        public string ToolName { get; } = "Landmarks";
    }
}