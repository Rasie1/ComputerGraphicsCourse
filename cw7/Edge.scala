sealed abstract class Side 

case object Left extends Side
case object Right extends Side
case object Center extends Side

case class Edge(a: (Int, Int), b: (Int, Int)) {
    def compareSide(a: Int, b: Int) : Side = {
        // todo
        if (a < b)
            return Left
        if (a > b)
            return Right
        return Center
    }

    def pointSide(point: (Int, Int)) : Side = (point, a, b) match {
        case ((x, y), (aX, aY), (bX, bY)) => {
            if (aY < bY)
                return compareSide(x, aX)
            return Center
        }
    }
}
