object Main extends App { 
    var edges = List(
        Edge((0, 0), (0, 2)),
        Edge((0, 2), (2, 2)),
        Edge((2, 2), (1, 1)),
        Edge((1, 1), (2, 0)),
        Edge((2, 0), (0, 0)))

    var polygon = new Polygon(edges)

    var point1 = (1.0, 1.0)
    var point2 = (5.0, 5.0)
    var point3 = (0.5, 0.5)

    Console.println("Многоугольник");
    Console.println(edges);

    Console.println("Положение точки (1.0, 1.0) относительно ребер")
    Console.println(edges map (e => e.pointSide(point1)))

    Console.println("Принадлежит ли (1.0, 1.0)")
    Console.println(polygon contains point1)

    Console.println("Принадлежит ли (5.0, 5.0)")
    Console.println(polygon contains point2)

    Console.println("Принадлежит ли (0.5, 0.5)")
    Console.println(polygon contains point3)

    Console.println("Поворот всех ребер на 90 вокруг центра")
    Console.println(edges map (e => e.rotate90AroundCenter()))

    Console.println("Поворот всех ребер на 15 вокруг (1, 1)")
    Console.println(edges map (e => e.rotate(15, point1)))

    Console.println("Точка пересечения Edge((-1, -1), (1, 1)) и Edge((1, -1), (-1, 1))")
    Console.println(Edge((-1, -1), (1, 1)) intersect Edge((1, -1), (-1, 1)))

    Console.println("Точка пересечения Edge((-2, -1), (1, 1)) и Edge((1, -1), (-1, 1))")
    Console.println(Edge((-2, -1), (1, 1)) intersect Edge((1, -1), (-1, 1)))

    Console.println("Точка пересечения Edge((-2, -20), (1, 1)) и Edge((10, -1), (1, 1))")
    Console.println(Edge((-2, -20), (1, 1)) intersect Edge((10, -1), (1, 1)))
} 
