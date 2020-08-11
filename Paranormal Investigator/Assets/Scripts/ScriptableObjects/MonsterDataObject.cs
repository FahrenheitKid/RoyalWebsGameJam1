﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Common.Enums;




[System.Serializable]
public class MonsterData
{
    public MonsterCharacter monster;
    public bool hasHands;
    public bool hasFeet;


}
[CreateAssetMenu(menuName = "ScriptableObject/MonsterDataObject")]
public class MonsterDataObject : ScriptableObject
{
    public List<MonsterData> monsterList;

    static public List<string> randomNames = new List<string> {"Abigor","Ace","Achilles","Acid","Adieu","Adrian","Aegis","Agaliarept",
    "Agares","Agora","Agrippa","Ahya","Aisha","Ajikage","Akey","Akiblo","Akira","Al","Alan","Albert","Aldis","Alex","Alexander","Alfred",
    "Algoreo","Alice","Allen","Alllietta","Allocen","Alpha","Alquen","Alsace","Alto","Alucard","Alumina","Amdusias","Amia","Amnelis",
    "Amon","Amulet","Amy","Anabell","Anastasia","Andras","Andre","Andrealphus","Andrew","Andromalius","Angeline","Animus","Anita","Anna",
    "Annerose","Annis","Ansbach","Antaios","Antonio","Aphrodite","Apollo","Appleton","Aqua","Aragorn","Arbaux","Archie","Aria","Arias",
    "Ariel","Arina","Arion","Ark","Armaros","Artemis","Arthur","Asbestos","Ash","Astarte","Athena","Athetosis","Athos","August","Aura",
    "Aurelia","Auslese","Avan","Axis","Azazel","Baggins","Baghi","Bakala","Balalaika","Baldr","Baldwin","Balin","Ballad","Baltis",
    "Barak","Barbara","Barsom","Beatrice","Beauchette","Beaufort","Becky","Belinus","Bellatrix","Belle","Belphegor","Benten","Beowulf",
    "Bereth","Bertrand","Betty","Bianca","Bifrons","Billy","Birru","Bismarck","Blair","Bob","Bonaparte","Bonita","Boo","Boss","Bossanova","Bouquet","Bravo","Brenda","Brigit","Brocken","Bruce","Buelle","Byung","Cadmus","Cagliostro","Cameo","Cameron","Canard","Caradoc","Carbide","Carl","Carlos","Carlton","Carmalide","Carol","Carrie","Cathy","Caulang","Cecil","Cecila","Celcia","Chapeau","Charles","Charlotte","Charmant","Chen","Chiangley","Chicori","Chlordane","Choux","Christine","Christoph","Chuck","Ciel","Cinderella","Cinzano","Clarion","Claris","Clefford","Clipton","Clive","Cloak","Clove","Clown","Clutch","Codeine","Cognac","Comet","Connie","Coral","Cornerius","Cornet","Coronet","Corriedale","Corsage","Cotton","Couturiere","Crescentia","Croissant","Crouton","Cymbal","Cynthia","Damian","Dan","Dandelion","Dandy","Daphne","Darby","Debonair","Delia","Delta","Devon","Diana","Dietrich","Dilan","Djinn","Dog","Dogma","Dolce","Domitus","Don Juan","Donnie","Dora","Dorro","Douglas","Drew","Dschingis","Duke","Dustin","Dynamo","Ebonite","Eclair","Ecole","Ed","Edgar","Eida","Eigendorf","Eiji","Eisbein","Eisenach","Eisenerz","Elaine","Eleanor","Elena","Elfir","Elfriede","Eligor","Elisa","Elise","Elizabeth","Elphin","Elsa","Elsheimer","Ema","Emilio","Emmy","Empress","Engarde","Enigma","Epsilon","Erg","Erica","Ernst","Escargo","Esmerelda","Esparanto","Esprit","Estel","Ethel","Ethos","Etranger","Etwas","Eve","Exocet","Fabrizia","Faith","Falco","Falfie","Falquenne","Falus","Fatima","Faustina","Fay","Fei-Hung","Felicia","Fenella","Ferne","Ferri","Fiador","Fiona","Fixer","Flauros","Flea","Fleur","Flora","Florence","Fondue","Foon","Francine","Frank","Freda","Frederica","Freya","Friedrich","Fubuki","Fuke","Fyz","Gabbot","Gainer","Gair","Gairu","Galaxy","Gallant","Gambit","Gandalf","Gansel","Garcon","Gareth","Garnet","Garon","Garosh","Gash","Gaston","Gaufres","Gawein","Gaws","Gazette","Geese","Geist","Gemeiner","General","George","Geraint","Gerhard","Geronimo","Gesellschaft","Gestalt","Gewalt","Giko","Gillian","Gimlet","Gin","Ginger","Gingham","Gloria","Gnocchi","Goblet","Goemon","Goldie","Grace","Grammy","Gray","Great","Gremory","Griffon","Grune","Guenever","Gustav","Guy","Haken","Halver","Hannah","Hanzo","Harlequin","Harrison","Harry","Hartwin","Haydn","Hazuki","Hector","Hedwig","Heinrich","Heinz","Helen","Hellicios","Hermina","Hickory","Hilde","Homeros","Horchata","Hrothgar","Hulloc","Hun","Husky","Idea","Idola","Ilumina","Ingrid","Ipos","Ippril","Iris","Irma","Isabel","Ishtar","Isolde","Ivan","Jaccard","Jack","Jackal","Jam","James","Jane","Jarble","Jasmine","Jeanne","Jessica","Jesus","Jet","Jeyal","Jill","Jim","Joanna","Joclyn","Joe","Johann","Johanson","John","Johnson","Joker","Jordan","Jruu","Jubei","Judas","Judith","Julie","Julienne","Julietta","Julius","Justice","Kain","Kaiser","Kanna","Karen","Karin","Karla","Karrey","Kary","Kasekuchen","Kasuba","Kasumi","Kate","Katerine","Katrina",
    "Keeseling","Keim","Keith","Kennel","Kessler","Khmer","Kinkan","Kit","Kiwi","Klara","Klimina","Klomn","Kocher","Komugi","Konga",
    "Kosmos","Krajicek","Kriemhild","Kurt","Kyle","Kyrielich","Lachesis","Laelia","Lafeene","Lambda","Lana","Lancelot","Lancia","Lao",
    "Lapiz","Larugo","Laslo","Laudigan","Launceor","Laura","Lauren","Laverna","Leia","Leighton","Lemon","Lena","Lenn","Leon","Leticia",
    "Liberty","Liese","Ligia","Lily","Limbo","Lime","Linda","Linker","Lionel","Lisa","Lita","Logan","Lone Wolf","Lorelei","Love",
    "Lowell","Lucille","Lucretia","Ludwig","Luke","Luna","Lundi","Lunista","Luphina","Lutius","Lynn","Macky","Madonna","Magdalena",
    "Magenta","Maggie","Maggiore","Magnolia","Magnus","Malcolm","Malthus","Mao","Marchocias","Marco","Margarita","Mariah","Mariell",
    "Marin","Marissa","Marius","Mark","Marl","Marlone","Mars","Martin","Mary","Matilda","Matthias","Maximillian","Maximus","May",
    "Maya","Mayden","Mazurka","Medea","Meena","Megan","Meimu","Meindorf","Melchior","Melissa","Melody","Memory","Menuette","Mephisto",
    "Mercia","Merium","Merle","Meryl","Metiee","Meyer","Michelle","Mihail","Mike","Milan","Millia","Minerva","Ming-Sia","Mint","Mirage",
    "Misha","Mocchus","Mocci","Modero","Mohawk","Moira","Moloch","Monar","Mordred","Morpheus","Mort","Mourvin","Muireann","Musashi",
    "Mustafa","Muzari","Myrddin","Nadia","Napoleon","Natalie","Nebula","Neidhardt","Neige","Neirin","Nel","Nero","Nessler","Nicholas",
    "Nirva","Nitro","Noelle","Noin","Noir","Non","Nyah","Oath","Oce","Octavia","Odessia","Oidepus","Olga","Olias","Olive","Olivier",
    "Olympia","Omega","Oracle","Orivea","Orson","Oscar","Otto","Ouzo","Ozmyere","Palmiro","Pamela","Pamille","Pancho","Pastel","Patrick",
    "Paulman","Peetan","Penelope","Percival","Perfume","Perrin","Peter","Petite","Phobos","Phoenix","Phyllis","Pia","Pierina",
    "Pizzicato","Pock","Poette","Polly","Pollyanna","Pratima","Praxis","Primula","Pris","Pritny","Pucelle","Quess","Quilt",
    "Quinine","Qwerty","Rachel","Raelene","Ralph","Ran","Rana","Rangue","Rastel","Ray","Rayrord","Razz","Reckendorf","Red","Rem",
    "Remiel","Remy","Rena","Rhea","Rhett","Riali","Ribbon","Ricardo","Rich","Richard","Ripley","Robert","Robinson","Robyn","Rocielle",
    "Rockwell","Roderick","Roger","Rolan","Romeo","Ronica","Rose","Rosetta","Rosewood","Rouge","Roxanne","Rubashka","Ruger","Russell",
    "Ryan","Sabrina","Sabrosa","Sakura","Saladin","Salmun","Salsburg","Salvia","Samchay","Samson","Samuel","Sarafan","Sarah","Saratoga",
    "Saria","Sarome","Sasha","Sasuke","Savarin","Scarlet","Scherzo","Schia","Scotch","Sebastian","Sector","Seila","Seimei","Seito",
    "Selene","Sepia","Shade","Shadow","Shamrock","Sharon","Shelby","Sherbert","Sherry","Shidoshi","Shredder","Si","Sialon","Sibyl",
    "Sigma","Sigurd","Sigurd","Silica","Silver","Silvia","Siniud","Siren","Sirius","Skald","Skinner","Sludge","Snob","Solaris","Soleil",
    "Solis","Soliton","Sombart","Sonata","Sonette","Sonic","Sonic","Sophia","Souffle","Spade","Spartan","Spiral","Staden","Star",
    "Steilhang","Stigma","Stinger","Stout","Strawberry","Sunday","Sushi","Suzanna","Suzie","Suzu","Svana","Sweet","Tamarin","Tamia",
    "Tamiel","Tandoori","Tanpopo","Tanya","Tao","Tarte","Teresa","Terrine","Tesse","Theo","Theta","Thomas","Tiffany","Tigre","Tina",
    "Tinker","Titan","Torquay","Tranza","Trianna","Trinity","Tristan","Tristram","Turbo","Ulrich","Undine","Uranus","Uriel","Va",
    "Valencia","Valerius","Valgus","Vanessa","Vanessa","Vasquez","Vediva","Vega","Venus","Vera","Veritace","Verne","Verrier","Vicky",
    "Victoria","Vilma","Vine","Violette","Vladimir","Vodka","Volac","Waffle","Walter","Warren","Wasabi","Wendy","Werner","Wesker",
    "Wilhelm","Willaby","William","Willock","Windy","Wing","Winney","Wolf","X-14D3","Xabia","Xandria","Xe","Xenon","Xig","Yanyan",
    "Ygerne","York","Yuki","Yvette","Yvonne","Yvonne","Z","Zanac","Zebra","Zeeman","Zeke","Zenia","Zeolite","Zero","Zeus","Zeveck",
    "Zisakuzien","Ziska","Zodiac"
    };
    


    public MonsterData GetMonsterData(MonsterCharacter character)
    {
        
        return monsterList.FirstOrDefault(x => x.monster == character);
    }
    public bool HasHands(MonsterCharacter character)
    {
        MonsterData md = monsterList.First(x => x.monster == character);
        if(md== null) return false;

        return md.hasHands;
    }

    public bool HasFeet(MonsterCharacter character)
    {
        MonsterData md = monsterList.First(x => x.monster == character);
        if(md== null) return false;
        
        return md.hasFeet;
    }

     public Element GetElement(MonsterCharacter character)
    {
        MonsterData md = monsterList.First(x => x.monster == character);
        if(md== null) return Element.Earth;
        
        return Monster.GetElement(character);
    }

}
