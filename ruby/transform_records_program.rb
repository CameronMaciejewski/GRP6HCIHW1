require('json')
require_relative('transform_records.rb')

t = RecordTransformer.new
delta = t.store_records(2000)
puts "took #{delta} seconds"