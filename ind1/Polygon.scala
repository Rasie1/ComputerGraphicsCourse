import scala.collection.immutable.Set


class Polygon(val edges: List[Edge]) {

def toSet[A](list: List[A]): Set[A] =
  list.foldLeft(Set[A]())( (r,c) => r + c)

    def points(): List[(Float, Float)] = {

        val points: Set[(Float, Float)] = 
            edges.foldLeft(Set[(Float, Float)]()) { 
                case (acc, Edge(a, b)) => acc + (a, b) 
            }

        return points.toList
    }

    def contains(point: (Float, Float)) : Boolean = {
        val hugeNumber: Float = 90000000f
        val checkerEdge = Edge((hugeNumber, hugeNumber), point)

        val intersections = 
            edges map (e => e intersect checkerEdge) filter (_ != None)

        return intersections.size % 2 == 1
    }
}
