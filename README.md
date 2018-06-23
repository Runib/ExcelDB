# ExcelDB
=======================================================================================================================================
Program służy do tworzenia plików z rozszerzeniem .xls lub .xlsx, które zawierają arkusz z komórkami w formie tabeli. Tabela przyjmuje 
kształt nazwy kolumn wraz z rekordami. Traktowana jest jako pojedynca tabela w bazie danych. Oprócz tworzenia nowych plików, jest
możliwość ładowania istniejącacyh plików wraz z wszystkimi arkuszami/tabelami i wypełnionymi komórkami w odpowiednim formacie.
Aplikacja potrafi rozpoznawać podstawowe typy danych jak: Numeric, String oraz Boolean.

## Wymagania wstępne
=======================================================================================================================================
Projekt tworzony był z użyciem Visual Studio 2017. Wykorzystuje on bibliotekę NPOI pozwalającą na operację na plikach z rozszerzeniem
.xls oraz .xlsx. Wczytany może zostać dowolny plik z tym rozszerzeniem.

## Ustawienia projektu
Domyślnie ścieżka zapisu nowego pliku to dysk D. Jeśli chcemy zmienić scieżkę zapisu, należy edytować plik CreateBaseViewModel.cs
i zmienić ściężkie na inną.

## Korzystanie z programu
- Program daje możliwość tworzenia nowego pliku lub wczytania istniejącego pliku z rozszerzeniem .xls oraz .xlsx.
- W przypadku tworzenia nowego pliku, użytkownik jest instruowany tak, by stworzony plik miał odpowiednią formę.
- W przypadku ładowania pliku do programu, wystarczy aby plik nie był uszkodzony oraz miał odpowiednie rozszerzenie. To jak zostaną 
wyświetlone dane, zależy od zawartości pliku.
- Stworzony lub wczytany plik można modyfikować lub wykonywać na nim kilka operacji. Dane znajdujące się w pliku jeśli 
posiadają odpowiedni format zostaną wyświetlone. Działanie na rekordach pliku obsługiwane jest za pomocą przycisków:
  * Dodaj - dodaje nowe rekordy do pliku uwzgledniając typ kolumny
  * Wyszukaj - wyszukuje odpowiedniej frazy w kolumnach i wynik wyświetla w oknie wyświetlania
  * Sortuj - sortuje dane i wynik wyświetla w oknie wyświetlania
  * Usuń - usuwa zaznaczony rekord

## Autorzy
Rafał Niemczyk - [Runib](https://github.com/Runib)

## Licencja
Projekt dostępny jest na licencji MIT, sprawdź [LICENSE](LICENSE), aby dowiedzieć się wszystkich szczegółów

## Biblioteka NPOI
- Program korzysta z darmowej biblioteki NPOI. Więcej szczegółów znajduję się tutaj. [NPOI](https://github.com/tonyqus/npoi)
