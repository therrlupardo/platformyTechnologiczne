package platformy.technologiczne.lab4.controllers;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;
import org.springframework.web.util.UriComponentsBuilder;
import platformy.technologiczne.lab4.models.Orders;
import platformy.technologiczne.lab4.services.OrderService;
import platformy.technologiczne.lab4.services.exceptions.OutOfStockException;

import java.net.URI;
import java.util.List;
import java.util.UUID;

@RestController
@RequestMapping("/orders")
public class OrderController {
    private final OrderService orderService;

    public OrderController(OrderService orderService){
        this.orderService = orderService;
    }

    @GetMapping
    public List<Orders> listOrders(){
        return orderService.findAll();
    }


    @GetMapping("/{id}")
    public ResponseEntity<Orders> getOrder(@PathVariable UUID id){
        Orders order = orderService.find(id);

        if(order == null){
            return ResponseEntity.notFound().build();
        }
        else{
            return ResponseEntity.ok(order);
        }
    }

    @PostMapping
    public ResponseEntity<Void> addOrder(@RequestBody Orders order,
                                         UriComponentsBuilder uriComponentsBuilder){
        try{
            orderService.placeOrder(order);
            URI location = uriComponentsBuilder.path("/orders/{id}").buildAndExpand(order.getId()).toUri();
            return ResponseEntity.created(location).build();
        } catch (OutOfStockException e1){
            return ResponseEntity.unprocessableEntity().build();
        }
    }
}
