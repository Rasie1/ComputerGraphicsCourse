// Credit: https://github.com/mdda/DelaunayScala hugely helped me, thanks

object Delaunay {
  def triangulate(points : List[(Float, Float)]) : Seq[(Int, Int, Int)] = {   
    case class IntTriangle(p1:Int, p2:Int, p3:Int)
    case class IntEdge(p1:Int, p2:Int)    
    case class EdgeAnnigilationSet(s: Set[IntEdge]) {
      def add(e: IntEdge): EdgeAnnigilationSet = {
        if (s.contains(e)) {
          new EdgeAnnigilationSet(s - e)
        }
        else {
          val e_reversed = IntEdge(e.p2, e.p1)
          if (s.contains(e_reversed)) {
            new EdgeAnnigilationSet(s - e_reversed)
          }
          else {
            new EdgeAnnigilationSet(s + e)
          }
        }
      }
      def toList() = s.toList
    }
    
    val n_points = points.length
    
    // Find the maximum and minimum vertex bounds, to allow calculation of the bounding triangle
    val Pmin = (points.map(_._1).min, points.map(_._2).min)  // Top Left
    val Pmax = (points.map(_._1).max, points.map(_._2).max)  // Bottom Right
    val diameter = (Pmax._1 - Pmin._1) max (Pmax._2 - Pmin._2)
    val Pmid = ((Pmin._1 + Pmax._1)/2f, (Pmin._2 + Pmax._2)/2f)
  
    /*
      Set up the supertriangle, which is a triangle which encompasses all the sample points.
      The supertriangle coordinates are added to the end of the vertex list. 
      The supertriangle is the first triangle in the triangle list.
    */    
    val pointList = points ::: List(
      (Pmid._1 - 2f*diameter, Pmid._2 - 1f*diameter), 
      (Pmid._1 - 0f*diameter, Pmid._2 + 2f*diameter), 
      (Pmid._1 + 2f*diameter, Pmid._2 - 1f*diameter)
   )
   
    val mainCurrentTriangles = List(IntTriangle(n_points + 0, n_points + 1, n_points + 2))
    val mainCompletedTriangles: List[IntTriangle] = Nil
  
    def convertRelevantTrianglesIntoNewEdges(completedTriangles: List[IntTriangle], 
                                             triangles: List[IntTriangle], 
                                             point: (Float, Float)) =
      triangles.foldLeft((completedTriangles: List[IntTriangle], 
                          List[IntTriangle](), 
                          EdgeAnnigilationSet(Set.empty[IntEdge]))) {
        case ((completed, current, edges), triangle) => {
          // If the point 'point_being_added' lies inside the circumcircle then the three edges 
          // of that triangle are added to the edge buffer and that triangle is removed.
          
          // Find the coordinates of the points in this incomplete triangle
          val corner1 = pointList(triangle.p1)
          val corner2 = pointList(triangle.p2)
          val corner3 = pointList(triangle.p3)
          
          val circumcircle = new Circumcircle(corner1, corner2, corner3)
          val inside = circumcircle.inside(point)
          val circle = circumcircle.center
          val r = circumcircle.radius
          if (circle._1 + r < point._1) {  // (false &&) to disable the 'completed' optimisation
          // have we moved too far in x to bother with this one ever again? (initial point list must be sorted for this to work) if (circle._1 + r < point._1) {  // (false &&) to disable the 'completed' optimisation
            (triangle::completed, current, edges) // Add this triangle to the 'completed' accumulator, and don't add it on current list
          } else {
            if (inside) {
              val edgesWithTriangleAdded = 
                edges
                  .add(IntEdge(triangle.p1, triangle.p2))
                  .add(IntEdge(triangle.p2, triangle.p3))
                  .add(IntEdge(triangle.p3, triangle.p1))
              (completed, current, edgesWithTriangleAdded)
            } else {
              (completed, triangle::current, edges)  // This point was not inside this triangle - just add it to the 'current' list
            }
          }
        }
      }
  
    def updateTriangleListForNewPoint(completedTriangles: List[IntTriangle], triangles: List[IntTriangle], point_i: Int) = {
      val (completedTrianglesUpdated, current_triangles_updated, edges_created) = 
        convertRelevantTrianglesIntoNewEdges(completedTriangles, triangles, pointList(point_i))
        
      // Form new triangles for the current point, all edges arranged in clockwise order.
      val newTriangles = for (e <- edges_created.toList) yield IntTriangle(e.p1, e.p2, point_i)
      (completedTrianglesUpdated, newTriangles ::: current_triangles_updated)
    }
   
    // Go through points in x ascending order.  No need to sort the actual points, just output the point_i in correct sequence 
    // (relies on sortBy being 'stable' - so that sorting on y first will enable duplicate detection afterwards)
    val pointsSortedXYAscending = pointList.take(n_points).zipWithIndex sortBy(_._1._2) sortBy(_._1._1) map { case ((x,y), i) => i } 
    
    val pointsSortedAndDeduped = 
      pointsSortedXYAscending.foldLeft((Nil:List[Int], -1)) {
        case ((list, pointLast), point_i) => if (pointLast>=0 && pointList(pointLast) == pointList(point_i)) {
          (list, point_i)
        } else {
          (point_i::list, point_i) 
        }
      }._1 reverse 
    
    // Add each (original) point, one at a time, into the existing mesh
    val (finalCompleted, finalTriangles) = 
      pointsSortedAndDeduped.foldLeft((mainCompletedTriangles, mainCurrentTriangles)) {
        case ((completed, current), point_i) => updateTriangleListForNewPoint(completed, current, point_i)
      }
    
    val fullListOfTriangles = (finalCompleted ::: finalTriangles)
    fullListOfTriangles.filterNot(t => (t.p1 >= n_points || t.p2 >= n_points || t.p3 >= n_points)) map { t => (t.p1, t.p2, t.p3) } 
  }

}
