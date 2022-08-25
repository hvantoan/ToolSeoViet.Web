namespace TuanVu.Management.Web.Models {

    public class ImageDto {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public byte[] Data { get; set; }

        public ImageDto Clone(bool isCloneImage = true) {
            return new ImageDto {
                Id = this.Id,
                Name = this.Name,
                Image = isCloneImage ? this.Image : default,
                Data = this.Data,
            };
        }
    }
}