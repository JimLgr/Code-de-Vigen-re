using System.Text;
using System.Text.RegularExpressions;
using ConsoleAppCesar;

const int CODE_ASCII_A_MAJUSCULE = 97;
const int LONGUEUR_ALPHABET = 26;
char[] alphabet = new char[LONGUEUR_ALPHABET];
int limiteSuperieure = CODE_ASCII_A_MAJUSCULE + LONGUEUR_ALPHABET;
char[,] vigenere = new char[26, 26];

// Génération de l'alphabet A à Z
for (int lettre = CODE_ASCII_A_MAJUSCULE, cpt = 0; lettre < limiteSuperieure; lettre++, cpt++)
{
    alphabet[cpt] = (char)lettre;
}

for (int pDecalage = 0; pDecalage < LONGUEUR_ALPHABET; pDecalage++)
{
    for (int lettre = CODE_ASCII_A_MAJUSCULE, cpt = 0; lettre < limiteSuperieure; lettre++, cpt++)
    {
        alphabet[cpt] = (char)lettre;
    }
    // Traitement décalage
    int decalage = pDecalage;
    decalage = decalage % LONGUEUR_ALPHABET;
    string sAlphabet = new string(alphabet);
    string ligne = ConsoleAppCesar.Cesar.Chiffrer(sAlphabet, decalage);
    for (int indexOfChar = 0; indexOfChar < LONGUEUR_ALPHABET; indexOfChar++)
    {
        vigenere[pDecalage, indexOfChar] = ligne[indexOfChar];
    }
}
Console.WriteLine("Le tableau est rempli");


for (int indexOfLine = 0; indexOfLine < LONGUEUR_ALPHABET; indexOfLine++)
{
    for (int indexOfColumn = 0; indexOfColumn < LONGUEUR_ALPHABET; indexOfColumn++)
    {
        Console.Write($" {vigenere[indexOfLine, indexOfColumn]}");
    }
    Console.WriteLine();
}

// Demande pour Vigenere
Console.WriteLine("taper le texte à chiffrer");
string texteAChiffrer = Console.ReadLine();

Console.WriteLine("taper la clé de chiffrement");
string cleChiffrement = Console.ReadLine();

string texteChiffre = Chiffrer(texteAChiffrer, cleChiffrement);
Console.WriteLine(texteChiffre);

string Chiffrer(string pTexteAChiffrer,string pCle)
{
    string cleNormalisee = pCle.ToUpper();
    
    string texteMajuscule = pTexteAChiffrer.ToUpper();
    string texteNormalise = Regex.Replace(texteMajuscule, @"[^A-Z]", " ");
    StringBuilder sb = new StringBuilder();
    for (int indexOfChar = 0, indexOfCle = 0; indexOfChar < texteNormalise.Length; indexOfChar++)
    {
        if (indexOfCle >= cleNormalisee.Length)
        {
            indexOfCle = 0;
        }
        if (texteNormalise[indexOfChar] == ' ')
        {
            sb.Append(texteNormalise[indexOfChar]);
        }
        else
        {
            // Creer un alphabet décalé
            string alphabetAZ = new string(alphabet).ToUpper();
            int indexCharCle = alphabetAZ.IndexOf(cleNormalisee[indexOfCle], StringComparison.CurrentCultureIgnoreCase);
            // rechercher index du char dans l'alphabet A à Z
            int indexCharTexte = alphabetAZ.IndexOf(texteNormalise[indexOfChar], StringComparison.CurrentCultureIgnoreCase);
            int indexCharChiffre = (indexCharTexte + indexCharCle) % LONGUEUR_ALPHABET;
            sb.Append(alphabetAZ[indexCharChiffre]);
            //sb.Append(alphabetAZ[(alphabetAZ.IndexOf(texteNormalise[indexOfChar], StringComparison.CurrentCultureIgnoreCase) + alphabetAZ.IndexOf(cleNormalisee[indexOfCle], StringComparison.CurrentCultureIgnoreCase)) % LONGUEUR_ALPHABET]);
            indexOfCle++;
        }
    }
    return sb.ToString();
}

