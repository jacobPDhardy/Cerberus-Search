using Tree_Builder;

SearchTree search = new SearchTree("(\"a\" & \"b\") | ((\"c\" ^ \"d\") & (!(\"e\" & \"f\"))) | (\"g\" & \"h\")");
//SearchTree search2 = new SearchTree("(\"a()\" AND b) OR ((c XOR d) AND (NOT (e AND f))) OR (g AND h)");
//SearchTree search3 = new SearchTree("(\"a(\" AND b) OR ((c XOR d) AND (NOT (e AND f))) OR (g AND h)"); //NEED TO ADD ESCAPE ALGORITHM TO TOP LEVEL TRANSLATOR