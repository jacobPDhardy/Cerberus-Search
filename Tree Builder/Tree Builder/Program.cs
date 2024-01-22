using Tree_Builder;

SearchStatement search = new SearchStatement("(a AND b) OR ((c XOR d) AND (NOT (e AND f))) OR (g AND h)");
SearchStatement search2 = new SearchStatement("(\"a()\" AND b) OR ((c XOR d) AND (NOT (e AND f))) OR (g AND h)");
SearchStatement search3 = new SearchStatement("(\"a(\" AND b) OR ((c XOR d) AND (NOT (e AND f))) OR (g AND h)"); //NEED TO ADD ESCAPE ALGORITHM TO TOP LEVEL TRANSLATOR