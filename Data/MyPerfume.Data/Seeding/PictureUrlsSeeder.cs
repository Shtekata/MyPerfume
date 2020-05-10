namespace MyPerfume.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;
    using MyPerfume.Data.Models;

    public class PictureUrlsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            if (dbContext.PictureUrls.Any())
            {
                return;
            }

            var pictureUrls = new List<string>()
            {
                "https://geshevalstorage.blob.core.windows.net/pictures/Boucheron/Quatre/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Boucheron/Quatre/PourHomme/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Bvlgari/Aqva/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Bvlgari/Goldea/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/CalvinKlein/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/CalvinKlein/Euphoria/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/CalvinKlein/Euphoria/Adv/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/CalvinKlein/Euphoria/Blossom/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/CalvinKlein/Euphoria/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/CalvinKlein/Euphoria/Adv/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/CalvinKlein/One/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/CalvinKlein/Reveal/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/CarolinaHerrera/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/CarolinaHerrera/GoodGirl/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Allure/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Allure/Edt/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Allure/Edt/Adv/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Allure/Sensuelle/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Allure/Edt/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Allure/Edt/Adv/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Chance/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Chance/EauFraiche/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Chance/EauVive/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Chanel№5/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Chanel№5/Adv/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Chanel№5/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Chanel№5/Adv/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Chanel№5/3.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/CocoMademoiselle/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/CocoMademoiselle/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/CocoMademoiselle/Adv/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/CocoMademoiselle/Intense/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/CocoMademoiselle/Adv/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/CocoNoir/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Chanel/Gabrielle/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/Addict/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/Addict/Adv/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/Addict/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/Addict/Adv/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/DolceVita/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/Dune/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/Fahrenheit/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/Homme/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/HypnoticPoison/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/Jadore/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/Joy/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/MissDior/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/MissDior/AbsolutelyBlooming/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/PoisonGirl/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/ChristianDior/Sauvage/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Dolce&Gabbana/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Dolce&Gabbana/TheOne/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Dolce&Gabbana/TheOne/Adv/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Dolce&Gabbana/TheOne/Grey/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Dolce&Gabbana/TheOne/Adv/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/GiorgioArmani/AcquaDiGio/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/GiorgioArmani/AcquaDiGio/Profumo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/GiorgioArmani/AcquaDiGioia/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/GiorgioArmani/Code/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/GiorgioArmani/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/GiorgioArmani/Si/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/GiorgioArmani/Si/Adv/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/GiorgioArmani/Si/Intense/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/GiorgioArmani/Si/Passione/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/GiorgioArmani/Si/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/GiorgioArmani/Si/Adv/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Givenchy/AngeNoir/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Givenchy/AngeOuDemon/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Givenchy/AngeOuDemon/Adv/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Givenchy/AngeOuDemon/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Givenchy/AngeOuDemon/Adv/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Givenchy/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Givenchy/Veryrresistible/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Gucci/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Gucci/Guilty/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Gucci/Guilty/Black/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Guerlain/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Guerlain/Insolence/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Guerlain/LaPetiteRoberNoir/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Guerlain/Mon/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Guerlain/Mon/Adv/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Guerlain/Mon/Florale/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Guerlain/Mon/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Guerlain/Mon/Adv/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Hermes/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Hermes/Terre/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/JeanPaulGaultier/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/JeanPaulGaultier/Scandal/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/KarlLagerfeld/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/KarlLagerfeld/ForHer/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Kenzo/Amour/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Kenzo/Amour/Adv/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Kenzo/Amour/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Kenzo/Amour/Adv/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Kenzo/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Kenzo/Flower/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Kenzo/JungleElephant/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Kenzo/World/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Lancôme/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Lancôme/Hypnose/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Lancôme/Hypnose/Adv/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Lancôme/Hypnose/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Lancôme/Hypnose/Adv/2.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/Lancôme/LaVieEstBelle/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/NarcisoRodriguez/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/NarcisoRodriguez/ForHer/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/NarcisoRodriguez/Poudree/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/NinaRicci/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/NinaRicci/L'Extase/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/NinaRicci/PremierJour/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/PacoRabanne/1Million/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/PacoRabanne/1Million/Cologne/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/PacoRabanne/1Million/Prive/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/PacoRabanne/Common/Logo/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/PacoRabanne/LadyMillion/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/PacoRabanne/LadyMillion/Prive/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/PacoRabanne/Olympea/1.jpg",
                "https://geshevalstorage.blob.core.windows.net/pictures/RobertoCavalli/Common/Logo/1.jpg",
            };

            foreach (var pictureUrl in pictureUrls)
            {
                var substring = pictureUrl.Substring(55);
                var match1 = Regex.Match(substring, @"\G([\w\d'№&ô]+)/?");
                var match2 = match1.NextMatch();
                var match3 = match2.NextMatch();

                var designerName = match1.Groups[1].Value;
                var perfumeName = match2.Groups[1].Value;
                var thirdItem = match3.Groups[1].Value;
                var fourItem = string.Empty;

                var pictureNumber = string.Empty;
                var additionalInfo = string.Empty;
                var secondAdditionalInfo = string.Empty;
                if (thirdItem.Length == 1)
                {
                    pictureNumber = thirdItem;
                }
                else
                {
                    additionalInfo = thirdItem;
                    var match4 = match3.NextMatch();
                    fourItem = match4.Groups[1].Value;
                    if (fourItem.Length == 1)
                    {
                        pictureNumber = fourItem;
                    }
                    else
                    {
                        secondAdditionalInfo = fourItem;
                        var match5 = match4.NextMatch();
                        pictureNumber = match5.Groups[1].Value;
                    }
                }

                await dbContext.PictureUrls.AddAsync(new PictureUrl
                {
                    Url = pictureUrl,
                    DesignerName = designerName,
                    PerfumeName = perfumeName,
                    PictureNumber = int.Parse(pictureNumber),
                    AdditionalInfo = additionalInfo,
                    SecondAdditionalInfo = secondAdditionalInfo,
                });
            }
        }
    }
}
