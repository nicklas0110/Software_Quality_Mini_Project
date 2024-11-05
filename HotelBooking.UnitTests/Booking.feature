Feature: Create Booking
  In order to avoid double bookings
  As a hotel manager
  I want to ensure that a room can only be booked if it is available

  Background:
    Given the following rooms are available
      | RoomID | Description |
      | 101    | Single      |
      | 102    | Double      |

    And the following customers are registered
      | CustomerID | Name       | Email             |
      | 1          | John Doe   | johndoe@email.com |
      | 2          | Jane Smith | janesmith@email.com |

  Scenario: Successfully create a booking when the room is available
    Given customer 1 requests a booking for room 101
    When the customer tries to book from "2025-01-10" to "2025-01-15"
    Then the booking should be successful

  Scenario: Fail to create a booking when the room is not available
    Given room 102 is already booked from "2025-01-10" to "2025-01-15"
    And customer 1 requests a booking for room 102
    When the customer tries to book from "2025-01-10" to "2025-01-15"
    Then the booking should be unsuccessful
