class Polygon(edges: List[Edge]) {
    def contains(point: (Double, Double)) : Boolean = {
        val hugeNumber: Double = 90000000000000.0 
        val checkerEdge = Edge((hugeNumber, hugeNumber), point)

        val intersections = 
            edges map (e => e intersect checkerEdge) filter (_ != None)

        return intersections.size % 2 == 1
    }
}
