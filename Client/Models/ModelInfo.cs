using Client.Enums;

namespace Client.Models
{
    public class ModelInfo
    {
        public ModalType ModalType { get; set; }
        public User User { get; set; }
        public List<string> Types { get; set; }
    }
}

