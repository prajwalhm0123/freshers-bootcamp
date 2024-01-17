import java.util.ArrayList;
import java.util.List;
import java.util.Set;
import java.util.function.Function;
import java.util.function.Predicate;
import java.util.stream.Collectors;

public class Main {
    public static List<String> filterList(List<String> words, Predicate<String> criteria) {
        return words.stream().filter(criteria).collect(Collectors.toList());
    }

    public static void printListToConsole(List<String> list) {
        for (String item : list) {
            System.out.print(item + ", ");
        }
        System.out.println();
    }

    public static Predicate<String> checkStringStartWithAny(String startString) {
        return stringItem -> stringItem.startsWith(startString);
    }

    public static void main(String[] args) {
        List<String> words = List.of("Tiger", "Lion", "Ranger", "Dictator", "Blue", "Foo");

        Predicate<String> endsWithVowel = s -> Set.of('a', 'e', 'i', 'o', 'u').contains(s.charAt(s.length() - 1));

        List<String> wordsEndingInVowel = filterList(words, endsWithVowel);

        Predicate<String> startsWithR = checkStringStartWithAny("R");
        List<String> wordsStartWithR = filterList(words, startsWithR);

        printListToConsole(wordsStartWithR);
        printListToConsole(wordsEndingInVowel);
    }
}
