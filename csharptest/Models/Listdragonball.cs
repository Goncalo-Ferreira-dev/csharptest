namespace csharptest.Models

{

    public class Personagem

    {

        public string name { get; set; }

        public string id { get; set; }

        public string imgUrl { get; set; }

    }


    public class DragonBallApiResponse

    {

        public Nome name { get; set; }

        public string id { get; set; }

        public Img image { get; set; }

    }


    public class Nome

    {

        public string official { get; set; }

    }


    public class Img

    {

        public string png { get; set; }

    }

}