package platformy.technologiczne.lab4.services;


import org.springframework.stereotype.Service;
import platformy.technologiczne.lab4.models.Movies;
import platformy.technologiczne.lab4.models.OrderedMovies;
import platformy.technologiczne.lab4.models.Orders;
import platformy.technologiczne.lab4.services.exceptions.OrderEmptyException;
import platformy.technologiczne.lab4.services.exceptions.OutOfStockException;

import javax.persistence.EntityManager;
import javax.transaction.Transactional;
import java.util.List;

@Service
public class OrderService extends EntityService<Orders> {

    public OrderService(EntityManager em){
        super(em, Orders.class, Orders::getId);
    }

    public List<Orders> findAll(){
        return em.createNamedQuery(Orders.findAll, Orders.class).getResultList();
    }

    /**
     *
     * @param order order to be made
     * @throws OrderEmptyException exception threw, when order is empty
     * @throws OutOfStockException exception threw, when is stock is out of some element of order
     */
    @Transactional
    public void placeOrder(Orders order) throws OrderEmptyException, OutOfStockException {
        if(order == null){
            throw new OrderEmptyException();
        }
        else if (order.getOrderedMovies().size() == 0){
            throw new OrderEmptyException();
        }

        for(OrderedMovies orderedMovies : order.getOrderedMovies()){
            Movies movie = em.find(Movies.class, orderedMovies.getMovie().getId());

            if(movie.getAmount() < orderedMovies.getAmount()){
                throw new OutOfStockException();
            }
            else {
                int new_amount = movie.getAmount() - orderedMovies.getAmount();
                movie.setAmount(new_amount);
            }

            save(order);

        }

    }


}
