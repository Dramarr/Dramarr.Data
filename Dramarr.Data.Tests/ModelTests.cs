using Dramarr.Data.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Dramarr.Data.Tests
{
    [TestClass]
    public class ModelTests
    {
        private readonly Guid ShowId = Guid.NewGuid();

        [TestMethod]
        public void ShouldCreateEpisode()
        {
            var url = "https://azvideo.net/go.php?IV4yMvbXCueTwkB/jiYzFLn6pugjm0gQj2/wLftPwNOcPmsJ/R9IA88Yf+CzozmgD9qJ5J0JTPWC3Jp0pBz7UOyGB4UhvT7av2C02zM9WUX2Lu1Nmy4H1/JHqrLtnOPVzzdyAj7+s27EIZ+LUAbkumPCqLgPaBNHvA0HYGsJAAHZiFBp7VpJr3tboZrPiOlivz4/kvv+ncVC7SNpv/Urug==";
            var filename = "Dr.Romantic.2.E01.mp4";

            var episode = new Episode(ShowId, url, filename);

            Assert.IsNotNull(episode);
        }

        [TestMethod]
        public void ShouldCreateLog()
        {
            var logType = Core.Enums.LogHelpers.LogType.DEBUG;
            var mesage = "message";
            var properties = "properties";

            var log = new Log(logType, mesage, properties);

            Assert.IsNotNull(log);
        }

        [TestMethod]
        public void ShouldCreateMetadata()
        {
            var imageUrl = "https://www.estrenosdoramas.net/wp-content/img/400.jpg";
            var plot = "Ikeuchi Aya es una chica ordinaria de 15 años de edad, hija de una familia que trabaja en una tienda de tofu y que pronto entrará a la preparatoria. Sin embargo, cosas extrañas le han estado ocurriendo a Aya últimamente. Se ha estado cayendo muy seguido y camina de manera muy extraña. Su madre, Shioka, lleva a Aya a ver a un médico y él le informa que Aya tiene una degeneración espinocerebral. Es una terrible enfermedad incurable en la cual el cerebelo gradualmente se va deteriorando hasta el punto en que la persona ya no podrá caminar, hablar, escribir o comer de manera normal pero que no afecta a la mente. Al igual que en su vida existen personas que le hagan daño existira personas que la apoyaran como Haruto, que la ayuda en los momentos de dolor y le da el aliento que necesite. ¿Cómo terminará esta historia?";
            var cast = "";
            var language = "Spanish";

            var metadata = new Metadata(ShowId, imageUrl, plot, cast, language);

            Assert.IsNotNull(metadata);
        }

        [TestMethod]
        public void ShouldCreateMyAsianTvShow()
        {
            var url = "https://myasiantv.to/drama/crash-landing-on-you/?utm_source=top_day&utm_medium=sidebar&utm_campaign=tracking";
            var show = new Show(url);

            Assert.AreEqual(show.Source, Core.Enums.SourceHelpers.Source.MYASIANTV);
            Assert.AreEqual(show.Title, "Crash Landing On You");
            Assert.AreEqual(show.Url, "crash-landing-on-you");
            Assert.AreEqual(show.Download, false);
            Assert.AreEqual(show.Enabled, true);
        }

        [TestMethod]
        public void ShouldCreateKshowShow()
        {
            var url = "https://kshow.to/shows/hello-counselor/";
            var show = new Show(url);

            Assert.AreEqual(show.Source, Core.Enums.SourceHelpers.Source.KSHOW);
            Assert.AreEqual(show.Title, "Hello Counselor");
            Assert.AreEqual(show.Url, "hello-counselor");
            Assert.AreEqual(show.Download, false);
            Assert.AreEqual(show.Enabled, true);
        }

        [TestMethod]
        public void ShouldCreateEstrenosDoramasShow()
        {
            var url = "https://www.estrenosdoramas.net/2016/06/1-rittoru-no-namida.html";
            var show = new Show(url);

            Assert.AreEqual(show.Source, Core.Enums.SourceHelpers.Source.ESTRENOSDORAMAS);
            Assert.AreEqual(show.Title, "1 Rittoru No Namida");
            Assert.AreEqual(show.Url, "/2016/06/1-rittoru-no-namida.html");
            Assert.AreEqual(show.Download, false);
            Assert.AreEqual(show.Enabled, true);
        }

    }
}
