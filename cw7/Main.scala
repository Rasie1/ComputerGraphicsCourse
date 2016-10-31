object Main extends App { 
    var edges = List(
        Edge((0, 0), (0, 2)),
        Edge((0, 2), (2, 2)),
        Edge((2, 2), (1, 1)),
        Edge((1, 1), (2, 0)),
        Edge((2, 0), (0, 0)))

    var polygon = new Polygon(edges)

    var point = (1, 1)

    Console.println(edges map (e => e.pointSide(point)))
} 
