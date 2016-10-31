object Sides extends Enumeration {
    val Right, Left = Value
}

class Polygon(edges: List[(Int, Int)]) {
    def pointSides(a: (Int, Int)) : List[Int] = a match {
        case (x, y) => edges.map(
            case (currentX, currentY) => 2)
    }
}
