sealed abstract class Side 

case object Left extends Side
case object Right extends Side
case object Center extends Side

case class Edge(a: (Double, Double), b: (Double, Double)) {
    def rotatePoint(center: (Double, Double), angle: Double, 
                    point: (Double, Double)) : (Double, Double) = {
        var s = Math.sin(angle)
        var c = Math.cos(angle)
  
        var (px, py) = point
        var (cx, cy) = center

        px = px - cx
        py = py - cy
  
        var xnew = px * c - py * s
        var ynew = px * s + py * c
  
        px = xnew + cx
        py = ynew + cy

        return (px, py)
    }

    def rotate(angle: Double, center: (Double, Double)) : Edge = {
        return Edge(rotatePoint(center, angle, a), 
                    rotatePoint(center, angle, b))
    }

    def center() : (Double, Double) = {
        (a, b) match {
            case ((aX, aY), (bX, bY)) => 
                ((aX + bX) / 2, (aY + bY) / 2)
        }
    }

    def rotate90AroundCenter() : Edge = {
        return rotate(90, center())
    }

    def intersect(other: Edge) : Option[(Double, Double)] = { 
        (other, a, b) match {
            case (Edge((p0_x, p0_y), (p1_x, p1_y)), (p2_x, p2_y), (p3_x, p3_y)) => {
                var s10_x = p1_x - p0_x
                var s10_y = p1_y - p0_y
                var s32_x = p3_x - p2_x
                var s32_y = p3_y - p2_y

                var denom = s10_x * s32_y - s32_x * s10_y
                if (denom == 0)
                    return None
                var denomPositive = denom > 0;

                var s02_x = p0_x - p2_x
                var s02_y = p0_y - p2_y
                var s_numer = s10_x * s02_y - s10_y * s02_x
                
                if ((s_numer < 0) == denomPositive)
                    return None

                var t_numer = s32_x * s02_y - s32_y * s02_x
                if ((t_numer < 0) == denomPositive)
                    return None

                if (((s_numer > denom) == denomPositive) || ((t_numer > denom) == denomPositive))
                    return None

                var t = t_numer / denom

                return Some(p0_x + (t * s10_x), 
                            p0_y + (t * s10_y))
            }
        }
    }

    def compareSide(a: Double, b: Double) : Side = {
        // todo
        if (a < b)
            return Left
        if (a > b)
            return Right
        return Center
    }

    def pointSide(point: (Double, Double)) : Side = (point, a, b) match {
        case ((x, y), (aX, aY), (bX, bY)) => {
            if (aY < bY)
                return compareSide(x, aX)
            return Center
        }
    }
}
